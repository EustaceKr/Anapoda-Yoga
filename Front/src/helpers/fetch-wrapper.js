import { useAuthStore } from '@/stores';

export const fetchWrapper = {
    get: (url) => request(url,null,'GET'),
    post:(url,body) => request(url,body,'POST'),
    put:(url,body) => request(url,body,'PUT'),
    delete:(url,body) => request(url,body,'DELETE')
};

async function request(url,body,method) {
    const requestOptions = {
        method,
        headers: authHeader(url)
    };
    if (body) {
        requestOptions.headers['Content-Type'] = 'application/json';
        requestOptions.body = JSON.stringify(body);
    }
    
    var response = await fetch(url, requestOptions)
    if (response.status == '401' || response.status == '403'){
        logout();
    }
    var text = await response.text();
    if(text.length > 0 && response.status < 300){
        return {body: await JSON.parse(text) , status: response.status}
    }else{
        return {body:null, status: response.status}
    }
}

// helper functions

function authHeader(url) {
    // return auth header with jwt if user is logged in and request is to the api url
    const { user } = useAuthStore();
    const isLoggedIn = !!user?.token;
    const isApiUrl = url.startsWith(import.meta.env.VITE_API_URL);
    if (isLoggedIn && isApiUrl) {
        return { Authorization: `Bearer ${user.token}` };
    } else {
        return {};
    }
}

// function handleResponse(response) {
//     return response.text().then(text => {
//         const data = text && JSON.parse(text);
        
//         if (!response.ok) {
//             const { user, logout } = useAuthStore();
//             if ([401, 403].includes(response.status) && user) {
//                 // auto logout if 401 Unauthorized or 403 Forbidden response returned from api
//                 logout();
//             }

//             const error = (data && data.message) || response.statusText;
//             return Promise.reject(error);
//         }

//         return data;
//     });
// }    
