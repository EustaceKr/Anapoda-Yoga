import { fetchWrapper } from '@/helpers';

const baseUrl = `${import.meta.env.VITE_API_URL}`;

export default {
    async getAll() {
        var result = await fetchWrapper.get(`${baseUrl}/customers`)
        return result.body
    },
    async saveCustomer(firstName, lastName, phone, mobileNumber, dateOfBirth, sex, adress, city, state, postalCode, payDate, timesPerMonth, username, password) {
        var result = await fetchWrapper.post(`${baseUrl}/customers`, {
            firstName, lastName, phone, mobileNumber, dateOfBirth, sex, adress, city, state, postalCode, payDate, timesPerMonth
            , username, password
        })
        return result.status
    },
    async editCustomer(id, firstName, lastName, phone, mobileNumber, dateOfBirth, sex, adress, city, state, postalCode, payDate, timesPerMonth) {
        var result = await fetchWrapper.put(`${baseUrl}/customers/${id}`, { firstName, lastName, phone, mobileNumber, dateOfBirth, sex, adress, city, state, postalCode, payDate, timesPerMonth })
        await this.getAll();
        return result.status
    },
    async deleteCustomer(id) {
        var result = await fetchWrapper.delete(`${baseUrl}/customers/${id}`)
        await this.getAll();
        return result.status
    },
    async getInYogaClass(yogaClassId) {
        this.customers = { loading: true };
        var result = await fetchWrapper.get(`${baseUrl}/customers/inYogaClass/${yogaClassId}`)
        this.customers = result.body
        return result.status
    },
    async getNotInYogaClass(yogaClassId) {
        this.customers = { loading: true };
        var result = await fetchWrapper.get(`${baseUrl}/customers/notInYogaClass/${yogaClassId}`)
        this.customers = result.body
        return result.status
    }
}
