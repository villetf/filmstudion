

export async function makeLoginCall(username:string, password:string) {
   return fetch('http://localhost:5134/api/Users/authenticate', {
      method: 'POST',
      headers: {
         'Content-Type': 'application/json'
      },
      body: JSON.stringify({
         username: username,
         password: password
      })
   })
   .then(async (res) => {
      return {
         status: res.status,
         data: await res.json()
      }
   })
}