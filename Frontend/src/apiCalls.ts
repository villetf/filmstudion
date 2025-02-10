

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

export async function getAllFilms() {
   return fetch('http://localhost:5134/api/Films', {
      method: 'GET',
      headers: {
         'Authorization': localStorage.getItem('userGuid') ?? ''
      }
   })
   .then(async res => await res.json())
}

export async function getStudiosRentals() {
   return fetch('http://localhost:5134/api/mystudio/rentals', {
      method: 'GET',
      headers: {
         'Authorization': localStorage.getItem('userGuid') ?? ''
      }
   })
   .then(async res => await res.json())
}