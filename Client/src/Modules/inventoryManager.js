import { getToken } from "./authManager"

const _apiUrl = "/api/inventory"

export const getInventory = (id) => {
    return getToken().then(token => {
      return fetch(`${_apiUrl}/${id}`, {
        method: "GET",
        headers: {
          "Authorization": "Bearer " + token
        }
      })
      .then(res => {
        if (!res.ok) {
          throw new Error(res.statusText);
        }
        return res.json();
      })
      .catch(error => {
        console.error('There was an error!', error);
      });
    })
  }

export const getAllInventoryUserIds = () => {
    return getToken().then(token => {
        return fetch(`${_apiUrl}/getAllInvUserIds`, {
            method: "GET",
            headers: {
                "Authorization": "Bearer " + token
            }
        }).then(res => res.json())
    })
}

export const addInventory = (inventory) => {
    return getToken().then(token => {
        return fetch(`${_apiUrl}/addInv`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': "Bearer " + token
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

export const addFoodType = (food) => {
    return getToken().then(token => {
        return fetch(`${_apiUrl}/foodType`, {
            method: "POST",
            headers: {
                "Authorization": "Bearer " + token,
                "Content-Type": "application/json"
            },
            body: JSON.stringify( food )
        })
    })
}

export const DeleteFoodType = (id) => {
    return getToken().then(token => {
        return fetch(`${_apiUrl}/deleteFoodType/${id}`, {
            method: 'DELETE',
            headers: {
                'Authorization': "Bearer " + token
            }
        })
    });
}

export const addFood = (invFood) => {
    return getToken().then(token => {
        return fetch(`${_apiUrl}/food`, {
            method: "POST",
            headers: {
                "Authorization": "Bearer " + token,
                "Content-Type": "application/json"
            },
            body: JSON.stringify( invFood )
        })
    });
}

export const editFood = (food, foodId) => {
    return getToken().then(token => {
        return fetch(`${_apiUrl}/food/${foodId}`, {
            method: 'PUT',
            headers: {
                'Authorization': `Bearer ${token}`,
                "Content-Type": "application/json"
            },
            body: JSON.stringify( food )
        })
    })
}

export const DeleteFood = (id) => {
    return getToken().then(token => {
        return fetch(`${_apiUrl}/deleteFood/${id}`, {
            method: 'DELETE',
            headers: {
                'Authorization': "Bearer " + token
            }
        })
    });
}

export const getAllTools = () => {
    return getToken().then(token => {
        return fetch(`${_apiUrl}/tools`, {
            method: 'GET',
            headers: {
                Authorization: "Bearer " + token
            }
        }).then(res => res.json())
    })
}

export const addTool = (invTool) => {
    return getToken().then(token => {
        return fetch(`${_apiUrl}/tool`, {
            method: "POST",
            headers: {
                Authorization: `Bearer ${token}`,
                "Content-Type": "application/json"
            },
            body: JSON.stringify( invTool )
        })
    });
}

export const deleteTool = (toolId, invId) => {
    return getToken().then(token => {
        return fetch(`${_apiUrl}/deleteTool/${toolId}/${invId}`, {
            method: 'DELETE',
            headers: {
                'Authorization': "Bearer " + token
            }
        })
    });
}

export const getAllWeapons = () => {
    return getToken().then(token => {
        return fetch(`${_apiUrl}/weapons`, {
            method: 'GET',
            headers: {
                Authorization: "Bearer " + token
            }
        }).then(res => res.json())
    })
}

export const addWeapon = (invWeapon) => {
    return getToken().then(token => {
        return fetch(`${_apiUrl}/weapon`, {
            method: "POST",
            headers: {
                Authorization: "Bearer " + token,
                "Content-Type": "application/json"
            },
            body: JSON.stringify( invWeapon )
        })
    });
}

export const deleteWeapon = (weaponId, invId) => {
    return getToken().then(token => {
        return fetch(`${_apiUrl}/deleteWeapon/${weaponId}/${invId}`, {
            method: 'DELETE',
            headers: {
                'Authorization': "Bearer " + token
            }
        })
    });
}

export const getAllEnergies = () => {
    return getToken().then(token => {
        return fetch(`${_apiUrl}/energy`, {
            method: 'GET',
            headers: {
                Authorization: "Bearer " + token
            }
        }).then(res => res.json())
    })
}

export const addEnergy = (invEnergy) => {
    return getToken().then(token => {
        return fetch(`${_apiUrl}/energy`, {
            method: "POST",
            headers: {
                Authorization: "Bearer " + token,
                "Content-Type": "application/json"
            },
            body: JSON.stringify( invEnergy )
        })
    });
}

export const deleteEnergy = (energyId, invId) => {
    return getToken().then(token => {
        return fetch(`${_apiUrl}/deleteEnergy/${energyId}/${invId}`, {
            method: 'DELETE',
            headers: {
                'Authorization': "Bearer " + token
            }
        })
    });
}

export const getAllMiscellaneous = () => {
    return getToken().then(token => {
        return fetch(`${_apiUrl}/miscellaneous`, {
            method: 'GET',
            headers: {
                Authorization: "Bearer " + token
            }
        }).then(res => res.json())
    })
}

export const addMiscellaneous = (invMiscellaneous) => {
    return getToken().then(token => {
        return fetch(`${_apiUrl}/miscellaneous`, {
            method: "POST",
            headers: {
                Authorization: "Bearer " + token,
                "Content-Type": "application/json"
            },
            body: JSON.stringify( invMiscellaneous )
        })
    });
}

export const deleteMiscellaneous = (miscId, invId) => {
    return getToken().then(token => {
        return fetch(`${_apiUrl}/deleteMiscellaneous/${miscId}/${invId}`, {
            method: 'DELETE',
            headers: {
                'Authorization': "Bearer " + token
            }
        })
    });
}





//May not need, but here is the structure for EDITS:
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






