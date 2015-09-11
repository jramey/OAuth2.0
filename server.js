var express  = require('express');
var app      = express(); 								
var port  	 = process.env.PORT || 8080; 	
var bodyParser = require('body-parser');
var methodOverride = require('method-override');
var crypto = require('crypto');
var http = require('http');
var session = require('express-session');
var routes = require(__dirname + '/public/routes.js');
var directory = __dirname + '/public';

app.use(express.static(directory)); 

app.use(bodyParser.urlencoded({'extended':'true'})); 
app.use(bodyParser.json()); 
app.use(bodyParser.json({ type: 'application/vnd.api+json' })); 
app.use(methodOverride('X-HTTP-Method-Override')); 

app.use(session({
    name: 'OAuth',
    cookie: {
        expires: false,
        httpOnly: true
    },
    secret: crypto.randomBytes(20).toString('hex'),
    resave: true,
    saveUninitialized: true
}));

app.use(routes);
http.createServer(app).listen(8080);
console.log("App listening on port " + port);