import { createNewElement } from "./createNewElement";

export function insertLogoutButton() {
   const loginForm = document.getElementById('login')!;
   loginForm.innerHTML = '';
   const logoutButton = createNewElement('button', 'Logga ut', null, 'text-base border bg-gray-200 ml-3 rounded-md px-3 py-3 cursor-pointer', loginForm);
   logoutButton.onclick = () => {
      localStorage.removeItem('userGuid');
      location.reload();
   }
}