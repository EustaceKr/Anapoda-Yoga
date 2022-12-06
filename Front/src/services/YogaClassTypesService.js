import { fetchWrapper } from '@/helpers';

const baseUrl = `${import.meta.env.VITE_API_URL}`;

export default {
    async getAll() {
        var result = await fetchWrapper.get(`${baseUrl}/yogaclasstypes`)
        return result.body;
    },
    async saveClassType(name, description, capacity, duration) {
        var result = await fetchWrapper.post(`${baseUrl}/yogaclasstypes`, { name, description, capacity, duration })
        await this.getAll();
        return result.status
    },
    async editClassType(id, name, description, capacity, duration) {
        var result = await fetchWrapper.put(`${baseUrl}/yogaclasstypes/${id}`, { name, description, capacity, duration })
        await this.getAll();
        return result.status
    },
    async deleteClassType(id) {
        var result = await fetchWrapper.delete(`${baseUrl}/yogaclasstypes/${id}`)
        await this.getAll();
        return result.status
    }
}
