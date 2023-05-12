import { getToken } from "./authManager"

const _apiUrl = "/api/aichat"

export const getAllChatsByUserId = (userId) => {
  return getToken().then(token => {
    return fetch(`${_apiUrl}/user/${userId}`, {
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

export const addAiChat = (aiChat) => {
  return getToken().then(token => {
    return fetch(`${_apiUrl}/add`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': "Bearer " + token
      },
      body: JSON.stringify(aiChat)
    }).then((resp) => {
      if (resp.ok) {
        console.log("AiChat added successfully!")
        return resp.json();
      } else {
        throw new Error(
          "An error occurred while trying to add AiChat"
        );
      }
    });
  });
}

export const updateAiChat = (aiChat, aiChatId) => {
  return getToken().then(token => {
    return fetch(`${_apiUrl}/${aiChatId}`, {
      method: 'PUT',
      headers: {
        'Authorization': `Bearer ${token}`,
        "Content-Type": "application/json"
      },
      body: JSON.stringify(aiChat)
    })
  })
}

export const deleteAiChat = (id) => {
  return getToken().then(token => {
    return fetch(`${_apiUrl}/${id}`, {
      method: 'DELETE',
      headers: {
        'Authorization': "Bearer " + token
      }
    })
  });
}