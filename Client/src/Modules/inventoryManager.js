import { getToken } from "/authManager"

const _apiUrl = "/api/inventory"

export const getInventory = (id) => {
    return getToken().then(token => {
        // The query parameters are only added if an argument is provided for them
        return fetch(`${_apiUrl}/${id}`, {
            method: "GET",
            headers: {
                Authorization: `Bearer ${token}`
            }
        }).then(res => res.json())
    })
}

export const addInventory = (inventory) => {
    return getToken().then(token => {
        return fetch(`${_apiUrl}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify(inventory)
        }).then((resp) => {
            if (resp.ok) {
                console.log("Inventory made successfully!")
                return resp.json();
            } else {
                throw new Error(
                    "An error occured while trying to add Inventory"
                );
            }
        });
    });
}

export const addFood = (food) => {
    return getToken().then(token => {
        return fetch(`${_apiUrl}`, {
            method: "POST",
            headers: {
                Authorization: `Bearer ${token}`,
                "Content-Type": "application/json"
            },
            body: JSON.stringify({ food })
        })
    })
}

export const editFood = (id, food) => {
    return getToken().then(token => {
        return fetch(`${_apiUrl}/${food}/${id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify(food)
        })
    });
}

export const editTool = (id, tool) => {
    return getToken().then(token => {
        return fetch(`${_apiUrl}/${tool}/${id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify(tool)
        })
    });
}

export const editWeapon = (id, weapon) => {
    return getToken().then(token => {
        return fetch(`${_apiUrl}/${weapon}/${id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify(weapon)
        })
    });
}

export const editEnergy = (id, energy) => {
    return getToken().then(token => {
        return fetch(`${_apiUrl}/${energy}/${id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify(energy)
        })
    });
}

export const editMiscellaneous = (id, miscellaneous) => {
    return getToken().then(token => {
        return fetch(`${_apiUrl}/${miscellaneous}/${id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify(miscellaneous)
        })
    });
}




