export const GPT = (gptObj) => {
    const myHeaders = new Headers();
    myHeaders.append("Authorization", "Bearer ");
    myHeaders.append("Content-Type", "application/json");
    var raw = JSON.stringify({
        "model": "gpt-3.5-turbo",
        "messages": [
            {
                "role": "system",
                "content": gptObj.systemContent
            },
            {
                "role": "user",
                "content": gptObj.userContent
            }
        ]
    });
    const requestOptions = {
        method: 'POST',
        headers: myHeaders,
        body: raw,
        redirect: 'follow'
    };
    return fetch("https://api.openai.com/v1/chat/completions", requestOptions)
        .then(response => response.json())
        .then(result => {
            console.log(result);
            return result;
        })
        .catch(error => console.log('error', error));
}

