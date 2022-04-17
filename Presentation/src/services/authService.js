import * as Constants from '../Constants/Common'

class AuthService {

  logout() {
    localStorage.removeItem("user");
  }

  async login(email, password) {
    return await this.#authenticate(email, password);
  }


  async register(email, password) {
    return await this.#authenticate(email,password, true);
  }

  async #authenticate(email, password, isRegister = false)
  {
    const requestOptions = {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ Email: email, Password: password })
    };

    return await fetch(`/api/Auth/${isRegister ? "register" : "login"}`, requestOptions)
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

        return `${isRegister ? Constants.successRegistered : Constants.successLogin }`;
  });
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