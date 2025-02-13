require('dotenv').config();
const express = require('express');
const app = express();
const port = 3000;

app.use(express.json());

app.use(express.static('.'));

app.get('/', (req,res) => {
    res.sendFile(__dirname+ '/index.html');
});



app.get('/api/ducks', async (req, res) => {
    try {
        const response = await fetch(`${process.env.API_URL}/ducks`);
        const ducks = await response.json();
        res.json(ducks);
    } catch (error) {
        console.error('Error when fetching ducks:', error);
        res.status(500).json({error: 'Cant fetch ducks from .NET API'});
    }
    
});

app.post('/api/ducks', async (req, res) => {
    try {
        const response = await fetch(`${process.env.API_URL}/ducks`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(req.body)
        });
        
        if(!response.ok) {
            throw new Error('Error when creating new duck');
        }
        
        const newDuck = await response.json();
        res.status(201).json(newDuck);
    } catch (error) {
        console.error('Fel vid skapande av anka', error);
        res.status(500).json({error: 'Could not create duck in .Net API'});
    }
});

app.listen(port, () => {
    console.log(`Server is running on port: ${port} `);
})