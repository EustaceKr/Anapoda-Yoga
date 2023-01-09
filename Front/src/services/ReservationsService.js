import { fetchWrapper } from '@/helpers';

const baseUrl = `${import.meta.env.VITE_API_URL}`;

export default {
    async getAllByUser() {
        var result = await fetchWrapper.get(`${baseUrl}/reservations`)
        return result.body
    },
    async saveReservation(yogaClassId) {
        var result = await fetchWrapper.post(`${baseUrl}/reservations`, { yogaClassId })
        return result.status
    },
    async deleteReservation(yogaClassId){
        var result = await fetchWrapper.delete(`${baseUrl}/reservations/?yogaClassId=${yogaClassId}`)
        return result.status
    },
    async adminSaveReservation(yogaClassId, customerId) {
        var result = await fetchWrapper.post(`${baseUrl}/reservations/admin`, { yogaClassId, customerId })
        return result.status
    },
    async adminDeleteReservation(yogaClassId, customerId) {
        var result = await fetchWrapper.delete(`${baseUrl}/reservations/admin?yogaClassId=${yogaClassId}&customerId=${customerId}`)
        return result.status
    }
}
