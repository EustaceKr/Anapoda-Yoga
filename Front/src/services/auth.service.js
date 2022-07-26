import axios from 'axios';
const API_URL = 'https://localhost:7198/api/';
class AuthService {
  login(user) {
    return axios
      .post(API_URL + 'Authenticate', {
        username: user.username,
        password: user.password
      })
      .then(response => {
        if (response.data.token) {
          localStorage.setItem('token', JSON.stringify(response.data.token));
        }
        return response.data.token
      });
  }
  logout() {
    localStorage.removeItem('token');
  }
  register(user) {
    return axios.post(API_URL + 'customers', {
      firstname: user.firstname,
      lastname: user.lastname,
      username: user.username,
      password: user.password
    });
  }
}
export default new AuthService();