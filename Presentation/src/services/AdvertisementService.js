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

    async saveAdvertisement(id) {
        const requestOptions = {
            method: 'POST',
            headers: {'Authorization': AuthService.getAuthHeader()},
        };

        return await fetch(`/api/Advertisements/${id}/save`, requestOptions)
            .then(async response => {
                return response.status === 200
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

    async getMySavedAdvertisments(pageIndex, pageSize = 10) {
        const requestOptions = {
            method: 'GET',
            headers: {'Content-Type': 'application/json', 'Authorization': AuthService.getAuthHeader()}
        };

        return await fetch(`/api/Advertisements/saved?PageIndex=${pageIndex}&PageSize=${pageSize}`, requestOptions)
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

    async getAllAdvertisments(request, pageIndex = 1, pageSize = 10, sortBy = 0) {
        const requestOptions = {
            method: 'GET',
            headers: {'Content-Type': 'application/json', 'Authorization': AuthService.getAuthHeader()},
        };
        
        let filterParams = new URLSearchParams();
        filterParams.append("PageIndex", pageIndex);
        filterParams.append("PageSize", pageSize);
        filterParams.append("SortBy", sortBy);
        if(request != null)
        {
            filterParams.append("MinPrice", request.minPrice);
            filterParams.append("MaxPrice", request.maxPrice);
            if(request.cityId > 0)
            {
                filterParams.append("CityId", request.cityId);
            }
            if(request.type > 0)
            {
                filterParams.append("Type", request.type);
            }
            if(request.isRent === "0" || request.isRent === "1")
            {
                filterParams.append("IsRent", !!parseInt(request.isRent));
            }
        }

        
        return await fetch(`/api/Advertisements?${filterParams}`, requestOptions)
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