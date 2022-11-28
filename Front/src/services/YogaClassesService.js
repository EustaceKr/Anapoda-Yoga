import { fetchWrapper } from '@/helpers';

const baseUrl = `${import.meta.env.VITE_API_URL}`;

export default {
    async getAll() {
        var result = await fetchWrapper.get(`${baseUrl}/yogaclasses`)
        return result.body
    },
    async saveClass(yogaClassTypeId, date) {
        var result = await fetchWrapper.post(`${baseUrl}/yogaclasses`, { yogaClassTypeId, date })
        return result
    },
    async editClass(id, yogaClassTypeId, date) {
        var result = await fetchWrapper.put(`${baseUrl}/yogaclasses/${id}`, { yogaClassTypeId, date })
        return result.status
    },
    async deleteClass(id) {
        var result = await fetchWrapper.delete(`${baseUrl}/yogaclasses/${id}`)
        return result.status
    }
}