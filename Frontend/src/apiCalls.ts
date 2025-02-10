

export function makeLogin(username:string, password:string) {
   fetch('http://localhost:5134/api/Users/authenticate', {
      method: 'POST',
      headers: {
         'Content-Type': 'application/json'
      },
      body: JSON.stringify({
         username: username,
         password: password
      })
   })
   .then(res => res.json())
   .then(data => data)
}