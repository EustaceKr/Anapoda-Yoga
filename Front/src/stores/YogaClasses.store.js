import { defineStore } from 'pinia';
import { fetchWrapper } from '@/helpers';

const baseUrl = `${import.meta.env.VITE_API_URL}`;

export const useClassesStore = defineStore({
    id: 'yogaclasses',
    state: () => ({
        yogaClasses: {}
    }),
    actions: {
        async getAll() {
            this.yogaClasses = { loading: true };
            var result = await fetchWrapper.get(`${baseUrl}/yogaclasses`)
            this.yogaClasses = result.body
        },
        async saveClass(yogaClassTypeId, date) {
            var result =  await fetchWrapper.post(`${baseUrl}/yogaclasses`, {yogaClassTypeId, date})
            await this.getAll();
            return result.status
        },
        async editClass(id,yogaClassTypeId, date){
            var result = await fetchWrapper.put(`${baseUrl}/yogaclasses/${id}`, {yogaClassTypeId, date})
            await this.getAll();
            return result.status
        },
        async deleteClass(id){
            var result =  await fetchWrapper.delete(`${baseUrl}/yogaclasses/${id}`)
            await this.getAll();
            return result.status
        }
    }
})