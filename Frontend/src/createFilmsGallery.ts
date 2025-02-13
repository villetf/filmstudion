import { mainContent } from "./main";
import { createNewElement } from "./createNewElement";
import { Film, Rental } from "./interfaces";
import { getFilmInfo, getStudiosRentals, postNewRental, postNewReturnal } from "./apiCalls";


export async function createFilmsGallery(allFilms:Film[], currentView:string) {
   let studioRentals:Rental[];
   const rentedIds:number[] = [];
   if (localStorage.getItem('studioId')) {
      studioRentals = await getStudiosRentals();
      studioRentals.forEach(rental => {
         rentedIds.push(rental.film.id);
      }); 
   }
   const filmsDiv = createNewElement('div', null, null, 'w-9/10 flex flex-wrap justify-center', mainContent);
   allFilms.forEach(film => {
      const filmDiv = createNewElement('div', null, null, 'w-70 mx-5 flex flex-col justify-center items-center border rounded-xl mb-10', filmsDiv);
      const posterImage = createNewElement('img', null, null, 'h-3/4 rounded-lg', filmDiv) as HTMLImageElement;
      posterImage.src = 'https://t4.ftcdn.net/jpg/02/12/52/91/360_F_212529193_YRhcQCaJB9ugv5dFzqK25Uo9Ivm7B9Ca.jpg';
      createNewElement('h4', `${film.name} (${film.releaseYear})`, null, 'font-bold text-xl mt-3', filmDiv);
      if (film.availableCopies == null) {
         return;
      }

      const copiesText = createNewElement('h5', `Antal tillgängliga exemplar: ${film.availableCopies} st`, null, null, filmDiv) as HTMLTitleElement;
      if (!localStorage.getItem('studioId')) {
         return;
      }
      const rentButton = createNewElement('button', 'Hyr', null, 'border cursor-pointer px-3 mt-1 rounded-lg', filmDiv) as HTMLButtonElement;
      insertRentButton(rentButton, copiesText, film, currentView);

      if (studioRentals && rentedIds.includes(film.id)) {
         insertReturnButton(rentButton, copiesText, film, currentView);
      }
   });   
}

function insertRentButton(rentButton:HTMLButtonElement, copiesText:HTMLTitleElement, film:Film, currentView:string) {
   rentButton.innerText = 'Hyr';   
   rentButton.onclick = async () => {
      const rentAction = await postNewRental(film.id);
      if (rentAction.status != 200) {
         alert(`${rentAction.data.message}`);
         return;
      }

      insertReturnButton(rentButton, copiesText, film, currentView);
      copiesText.innerText = `Antal tillgängliga exemplar: ${(await getFilmInfo(film.id)).availableCopies} st`;
   }
}

function insertReturnButton(rentButton:HTMLButtonElement, copiesText:HTMLTitleElement, film:Film, currentView:string) {
   rentButton.innerText = 'Lämna tillbaka';
   rentButton.onclick = async () => {
      const returnAction = await postNewReturnal(film.id);
      if (returnAction.status != 200) {
         alert(`${returnAction.data.message}`);
         return;
      }

      if (currentView == 'rented') {
         rentButton.parentElement?.remove();
      }

      insertRentButton(rentButton, copiesText, film, currentView);
      copiesText.innerText = `Antal tillgängliga exemplar: ${(await getFilmInfo(film.id)).availableCopies} st`;
   }
}