const express = require("express");
const cors = require("cors");
const bodyParser = require("body-parser");
require('dotenv').config();
const openai = require('openai');
openai.apiKey = process.env.OPENAI_API_KEY;
const { Configuration, OpenAIApi } = require("openai");




const config = new Configuration({
    apiKey: openai.apiKey
})

const openaiConfig = new OpenAIApi(config)


const app = express();
app.use(bodyParser.json());
app.use(cors());

app.post("/chat", async (req,res)=>{
    const {prompt} = req.body;

    const completion = await openaiConfig.createCompletion({
        model: "GPT-3.5-Turbo",
        // model: "text-davinci-003",
        max_tokens: 1000,
        temperature: 0,
        prompt: prompt,
    })
    res.send(completion.data.choices[0].text);
});

const PORT = 8020;

app.listen(PORT,() => {
    console.log(`server running on port : ${PORT}`);
})