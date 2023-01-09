import { defineStore } from 'pinia';
import { fetchWrapper, router } from '@/helpers';

const baseUrl = `${import.meta.env.VITE_API_URL}`;

export const useAuthStore = defineStore({
    id: 'auth',
    state: () => ({
        // initialize state from local storage to enable user to stay logged in
        user: JSON.parse(localStorage.getItem('user')),
        returnUrl: null
    }),
    actions: {
        async login(username, password) {
            const user = await fetchWrapper.post(`${baseUrl}/Authenticate`, { username, password });
            
            this.user = user.body;
            localStorage.setItem('userRole', JSON.stringify(user.body.userRole));
            localStorage.setItem('user', JSON.stringify(user.body));

            // redirect to previous url or default to home page
            router.push(this.returnUrl || '/');
        },
        logout() {
            this.user = null;
            localStorage.removeItem('user');
            localStorage.removeItem('userRole');
            router.push('/');
        },
        checkIfAdmin() {
            if(this.user){
                if (JSON.stringify(this.user.userRole) == '["Admin"]') return true
                else return false
            }
            else return false
        },
        checkIfUser(){
            if(this.user){
                if (JSON.stringify(this.user.userRole) == '["User"]') return true
                else return false
            }
            else return false
        }
    }
});
