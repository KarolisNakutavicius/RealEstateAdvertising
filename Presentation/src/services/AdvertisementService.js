import AuthService from "./AuthService";

class AdvertisementService {
    async createNewAdvertisement(request) {
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json', 'Authorization': AuthService.getAuthHeader() },
            body: JSON.stringify(request)
        };

        return await fetch(`/api/Advertisments`, requestOptions)
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

                return `Create Success`;
            });
    }

    async getMyAdvertisments()
    {
        const requestOptions = {
            method: 'GET',
            headers: { 'Content-Type': 'application/json', 'Authorization': AuthService.getAuthHeader() }
        };

        return await fetch(`/api/Advertisments`, requestOptions)
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

                return await response.json();
            });
    }

}
export default new AdvertisementService();