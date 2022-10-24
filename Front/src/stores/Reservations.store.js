import { defineStore } from 'pinia';
import { fetchWrapper } from '@/helpers';

const baseUrl = `${import.meta.env.VITE_API_URL}`;

export const useReservationsStore = defineStore({
    id: 'reservations',
    state: () => ({
        reservations: {}
    }),
    actions: {
        async getAllByUser() {
            this.reservations = { loading: true };
            var result = await fetchWrapper.get(`${baseUrl}/reservations`)
            this.reservations = result.body
        },
        async saveReservation(yogaClassId) {
            var result =  await fetchWrapper.post(`${baseUrl}/reservations`, {yogaClassId})
            return result.status
        }
    }
})