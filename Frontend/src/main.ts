import { createNewElement } from "./createNewElement";
import { insertLogoutButton } from "./helpers";
import { makeLogin } from "./makeLogin";
import { generateAllFilmsView } from "./generateAllFilmsView";
import { generateRentedView } from "./generateRentedView";

export const mainContent = document.getElementById('app')!;



if (localStorage.getItem('userGuid')) {
   insertLogoutButton();
   createNewElement('h1', 'Välkommen!', null, 'text-6xl mt-[35%]', mainContent)
} else {
   createNewElement('h1', 'Logga in uppe till höger', null, 'text-6xl mt-[35%]', mainContent)
}

document.getElementById('loginButton')!.onclick = () => {
   makeLogin();
}

document.getElementById('allfilms')!.onclick = () => {
   generateAllFilmsView();
}

document.getElementById('rented')!.onclick = () => {
   generateRentedView()
}

