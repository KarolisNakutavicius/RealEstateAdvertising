import axios from "axios";
import * as Constants from '../Constants/Common'

class AuthService {

  login(email, password) {

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

    return await fetch('/api/Auth/register', requestOptions)
      .then(async response => {
        if (response.status !== 200) {
          await response.json().then(data => {
            throw data[0].error;
          },
            error => {
              throw response.statusText;
            }
          )
        }

        this.#setTokenToStorage(response);

        return Constants.successRegistered;
      })
  }

  getCurrentUser() {
    return localStorage.getItem(Constants.tokenKey);
  }

  #setTokenToStorage(response) {
    response.json().then(data => {
      if (data.token) {
        localStorage.setItem(Constants.tokenKey, data.token)
      }
    })
  }

}
export default new AuthService();