import AuthService from "./AuthService";

class AdvertisementService {
    async createNewAdvertisement(request) {
        const requestOptions = {
            method: 'POST',
            headers: {'Authorization': AuthService.getAuthHeader()},
            body: request
        };

        return await fetch(`/api/Advertisements`, requestOptions)
            .then(async response => {
                if (response.status !== 200) {
                    await response.json().then(data => {
                            if (data[0].error) {
                                throw data[0].error
                            }

                            throw data.errors[0];
                        },
                        error => {
                            throw response.statusText;
                        }
                    )
                }

                return `Create Success`;
            });
    }

    async getMyAdvertisments(pageIndex, pageSize = 10) {
        const requestOptions = {
            method: 'GET',
            headers: {'Content-Type': 'application/json', 'Authorization': AuthService.getAuthHeader()}
        };

        return await fetch(`/api/Advertisements/mine?PageIndex=${pageIndex}&PageSize=${pageSize}`, requestOptions)
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

    async getAllAdvertisments(request) {
        const requestOptions = {
            method: 'GET',
            headers: {'Content-Type': 'application/json', 'Authorization': AuthService.getAuthHeader()},
        };

        let filterParams = new URLSearchParams(request)
        
        return await fetch(`/api/Advertisements${request ? `?${filterParams}` : ""}`, requestOptions)
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
            },
                error =>{
                console.log(error);
                debugger;
                });
    }

}

export default new AdvertisementService();