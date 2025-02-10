import { makeLoginCall } from "./apiCalls";
import { createNewElement } from "./createNewElement";
import { insertLogoutButton } from "./helpers";
import { mainContent } from "./main";

export async function makeLogin() {
   const username = (document.getElementById('username')! as HTMLInputElement).value;
   const password = (document.getElementById('password')! as HTMLInputElement).value;
   const answer = await makeLoginCall(username, password);
   if (answer.status != 200) {
      alert(answer.data.message);
      return;
   }

   localStorage.setItem('userGuid', answer.data.guid);
   insertLogoutButton();
   mainContent.innerHTML = '';
   createNewElement('h1', 'Välkommen!', null, 'text-6xl mt-[35%]', mainContent)
}