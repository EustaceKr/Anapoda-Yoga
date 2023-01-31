using Application.Services.CustomerImplementation;
using Data.Repositories.CustomerDAO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BackgroundServices
{
    public class TimesPerMonthUpdater : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public TimesPerMonthUpdater(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (IServiceScope scope = _serviceProvider.CreateScope())
                {
                    ICustomerService customerService =
                        scope.ServiceProvider.GetRequiredService<ICustomerService>();

                    var customers = await customerService.GetCustomers();

                    foreach (var customer in customers)
                    {
                        if (customer.PayDate != null)
                        {
                            if (DateTime.Now >= ((DateTime)customer.PayDate).AddMonths(1))
                            {
                                customer.TimesPerMonth = 0;
                                customerService.UpdateCustomer(customer);
                                customerService.Complete();
                            }
                        }
                    }
                    await Task.Delay(new TimeSpan(3,0,0));
                }
            }
        }
    }
}


