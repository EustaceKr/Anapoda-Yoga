import { fetchWrapper } from '@/helpers';

const baseUrl = `${import.meta.env.VITE_API_URL}`;

export default {

    async getAll() {
        var result = await fetchWrapper.get(`${baseUrl}/yogaclasses`)
        return result.body
    },
    async getAllByDate(selectedDate) {
        var result = await fetchWrapper.get(`${baseUrl}/yogaclasses/${selectedDate}`);
        return result?.body || [];
    },
    async saveClass(yogaClassTypeId, date, selectedDate) {
        var result = await fetchWrapper.post(`${baseUrl}/yogaclasses`, { yogaClassTypeId, date })
        await this.getAllByDate(selectedDate);
        return result.status
    },
    async editClass(id, yogaClassTypeId, date, selectedDate) {
        var result = await fetchWrapper.put(`${baseUrl}/yogaclasses/${id}`, { yogaClassTypeId, date })
        await this.getAllByDate(selectedDate);
        return result.status
    },
    async deleteClass(id, selectedDate) {
        var result = await fetchWrapper.delete(`${baseUrl}/yogaclasses/${id}`)
        await this.getAllByDate(selectedDate);
        return result.status
    }
}