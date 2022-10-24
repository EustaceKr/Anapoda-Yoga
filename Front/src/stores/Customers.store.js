import { defineStore } from 'pinia';
import { fetchWrapper } from '@/helpers';

const baseUrl = `${import.meta.env.VITE_API_URL}`;

export const useCustomersStore = defineStore({
    id: 'customers',
    state: () => ({
        customers: {}
    }),
    actions: {
        async getAll() {
            this.customters = { loading: true };
            var result = await fetchWrapper.get(`${baseUrl}/customers`)
            this.customers = result.body
        },
        async saveCustomer(firstName, lastName, username, password) {
            var result =  await fetchWrapper.post(`${baseUrl}/customers`, {firstName, lastName, username, password})
            await this.getAll();
            return result.status
        },
        async editClassType(id, firstName, lastName){
            var result = await fetchWrapper.put(`${baseUrl}/customers/${id}`, {firstName, lastName})
            await this.getAll();
            return result.status
        },
        async deleteClassType(id){
            var result =  await fetchWrapper.delete(`${baseUrl}/customers/${id}`)
            await this.getAll();
            return result.status
        }
    }
})