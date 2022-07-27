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
          localStorage.setItem('role', JSON.stringify(response.data.userRole))
        }
        return response.data.token
      });
  }
  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('role');
  }
}
export default new AuthService();