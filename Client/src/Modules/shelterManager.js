import { getToken } from "/authManager"

const _apiUrl = "/api/shelter"

export const getShelter = (shelterId) => {
    return getToken().then(token => {
        return fetch(`${_apiUrl}/${shelterId}`, {
            method: "GET",
            headers: {
                "Authorization": `Bearer ${token}`
            }
        }).then(res => res.json())
    })
}

export const addShelter = (shelter) => {
    return getToken().then(token => {
        return fetch(_apiUrl, {
            method: "POST",
            headers: {
                "Authorization": `Bearer ${token}`,
                "Content-Type": "application/json"
            },
            body: JSON.stringify(shelter)
        })
    })
}

export const editShelter = (shelter) => {
    return getToken().then(token => {
        return fetch(`${baseUrl}/${shelter.id}`, {
            method: "PUT",
            headers: {
                "Authorization": `Bearer ${token}`,
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                id: shelter.id,
                shelterMaterials: shelter.shelterMaterials,
                shelterPlan: shelter.shelterPlan 
            })
        })
        .then (res => res.json())
    })
}

export const deleteShelter = (shelterId) => {
    return getToken().then(token => {
        return fetch(`${_apiUrl}/${shelterId}`, {
            method: "DELETE",
            headers: {
                "Authorization": `Bearer ${token}`
            }
        })
    })
}