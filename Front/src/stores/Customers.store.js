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
            this.customers = { loading: true };
            var result = await fetchWrapper.get(`${baseUrl}/customers`)
            this.customers = result.body
        },
        async saveCustomer(firstName, lastName, username, password) {
            var result =  await fetchWrapper.post(`${baseUrl}/customers`, {firstName, lastName, username, password})
            await this.getAll();
            return result.status
        },
        async editCustomer(id, firstName, lastName){
            var result = await fetchWrapper.put(`${baseUrl}/customers/${id}`, {firstName, lastName})
            await this.getAll();
            return result.status
        },
        async deleteCustomer(id){
            var result =  await fetchWrapper.delete(`${baseUrl}/customers/${id}`)
            await this.getAll();
            return result.status
        },
        async getInYogaClass(yogaClassId) {
            this.customers = {loading: true};
            var result = await fetchWrapper.get(`${baseUrl}/customers/inYogaClass/${yogaClassId}`)
            this.customers = result.body
            return result.status
        },
        async getNotInYogaClass(yogaClassId) {
            this.customers = {loading: true};
            var result = await fetchWrapper.get(`${baseUrl}/customers/notInYogaClass/${yogaClassId}`)
            this.customers = result.body
            return result.status
        }
    }
})