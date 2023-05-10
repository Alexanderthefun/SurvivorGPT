import { getToken } from "/authManager"

const _apiUrl = "/api/curriculum"

export const getCurriculum = (userId) => {
    return getToken().then(token => {
        return fetch(_apiUrl, {
            method: "GET",
            headers: {
                "Authorization": `Bearer ${token}`
            }
        }).then(res => res.json())
    })
}

export const addCurriculum = (curriculum) => {
    return getToken().then(token => {
        return fetch(_apiUrl, {
            method: "POST",
            headers: {
                "Authorization": `Bearer ${token}`,
                "Content-Type": "application/json"
            },
            body: JSON.stringify(curriculum)
        })
    })
}

export const editCurriculum = (curriculum) => {
    return getToken().then(token => {
        return fetch(`${baseUrl}/${curriculum.id}`, {
            method: "PUT",
            headers: {
                "Authorization": `Bearer ${token}`,
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                id: curriculum.id,
                dailySchedule: curriculum.dailySchedule,
                dateCreated: curriculum.dateCreated 
            })
        })
        .then (res => res.json())
    })
}

export const deleteCurriculum = (curriculumId) => {
    return getToken().then(token => {
        return fetch(`${_apiUrl}/${curriculumId}`, {
            method: "DELETE",
            headers: {
                "Authorization": `Bearer ${token}`
            }
        })
    })
}