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
    
    async register(email, password) {
      const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ Email: email, Password: password })
      };
  
      await fetch('/api/Auth/register', requestOptions)
      .then(response =>
        {
          if(response.status !== 200)
          {
            throw response.statusText;
          }

          // save token
          return "User registere success"
        })
    }

    getCurrentUser() {
      return JSON.parse(localStorage.getItem('user'));;
    }

  }
  export default new AuthService();