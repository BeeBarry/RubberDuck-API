const express = require('express');
const app = express();
const port = 3000;

app.use(express.json());

app.get('/', (req,res) => {
    res.sendFile(__dirname+ '/index.html');
})

app.get('/api/test', (req, res) => {
    res.json({message: 'Hello from Express!'});
})

app.get('/api/ducks', async (req, res) => {
    //Koden för att hämta från .NET api
});

app.post('/api/ducks', (req, res) => {
   //Koden för att posta data till NET api 
});

app.listen(port, () => {
    console.log(`Server is running on port: ${port} `);
})