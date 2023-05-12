import { getToken } from "./authManager"

const _apiUrl = "/api/category"

export const getCategoryById = (id) => {
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

export const getAllCategories = () => {
  return getToken().then(token => {
    return fetch(`${_apiUrl}/getAll`, {
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

export const addCategory = (category) => {
  return getToken().then(token => {
    return fetch(`${_apiUrl}/add`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': "Bearer " + token
      },
      body: JSON.stringify(category)
    }).then((resp) => {
      if (resp.ok) {
        console.log("Category added successfully!")
        return resp.json();
      } else {
        throw new Error(
          "An error occurred while trying to add category"
        );
      }
    });
  });
}

export const updateCategory = (category, categoryId) => {
  return getToken().then(token => {
    return fetch(`${_apiUrl}/${categoryId}`, {
      method: 'PUT',
      headers: {
        'Authorization': `Bearer ${token}`,
        "Content-Type": "application/json"
      },
      body: JSON.stringify(category)
    })
  })
}

export const deleteCategory = (id) => {
  return getToken().then(token => {
    return fetch(`${_apiUrl}/${id}`, {
      method: 'DELETE',
      headers: {
        'Authorization': "Bearer " + token
      }
    })
  });
}