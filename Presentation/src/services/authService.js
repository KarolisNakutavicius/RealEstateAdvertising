import axios from "axios";

class AuthService {
    
    login(email, password) {
      return axios
        .post('auth/login', {
          username: email,
          password
        })
        .then(response => {
          if (response.data.accessToken) {
            localStorage.setItem("user", JSON.stringify(response.data));
          }
          return response.data;
        });
    }

    logout() {
      localStorage.removeItem("user");
    }
    
    register(email, password) {
      return axios.post('auth/register', {
        email,
        password
      });
    }

    getCurrentUser() {
      return JSON.parse(localStorage.getItem('user'));;
    }
  }
  
  export default new AuthService();