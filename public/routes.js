// code credit to Qualtrax/Huburn
// code can be found at https://github.com/Qualtrax/huburn/blob/master/lib/oauth-github/index.js

var uri = require('URIjs');
var request = require('request');
var express = require('express');
var crypto = require('crypto');
var app = module.exports = express();

app.get('/login/oauth', function (req, res) {
    req.session.oauth_csrf = crypto.randomBytes(20).toString('hex');
    res.redirect(new uri('http://localhost:50590/Login/OAuth/Authorize').query({
        client_id: 'OAuthClient',
        state: req.session.oauth_csrf
    }));
});

app.get('/login/oauth/callback', function (req, res) {
    if (req.query.state == undefined || req.query.state != req.session.oauth_csrf)
        res.send(404, 'Not Found');

    request({
        method: 'POST',
        url: 'http://localhost:50590/Login/OAuth/AccessToken',
        form: {
            client_id: "OAuthClient",
            client_secret: "SecretsDontMakeFriendsButTheyKeepThem",
            code: req.query.code
        }
    }, function (error, response, body) {
         if (error)
            res.send(500, 'Server error');
        console.log(body);
        var obj = JSON.parse(body);
        req.session.access_token = obj.access_token.Token;
        res.redirect('/user.html');     
    });
});

app.get('/user', function (req, res) {
   request({
    method: 'GET',
    url: 'http://localhost:50590/User',
    headers: {
      'Authorization': 'token ' + req.session.access_token
   }
  }).pipe(res);
});