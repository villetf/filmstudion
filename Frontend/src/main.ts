import { createNewElement } from "./createNewElement";
import { insertLogoutButton } from "./helpers";
import { makeLogin } from "./makeLogin";
import { generateAllFilmsView } from "./generateAllFilmsView";
import { generateRentedView } from "./generateRentedView";

export const mainContent = document.getElementById('app')!;



if (localStorage.getItem('userGuid')) {
   insertLogoutButton();
   createNewElement('h1', 'Välkommen!', null, 'text-6xl mt-[37vh]', mainContent)
   if (localStorage.getItem('studioId')) {
      document.getElementById('rented')?.classList.remove('hidden');
   }
} else {
   createNewElement('h1', 'Logga in uppe till höger', null, 'text-6xl mt-[37vh]', mainContent)
}

if (document.getElementById('loginButton')) {
   document.getElementById('loginButton')!.onclick = () => {
      makeLogin();
   }
} 

document.getElementById('allFilms')!.onclick = () => {
   generateAllFilmsView();
}

document.getElementById('rented')!.onclick = () => {
   generateRentedView()
}

