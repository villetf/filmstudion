import { mainContent } from "./main";
import { createNewElement } from "./createNewElement";
import { Film, Rental } from "./interfaces";
import { getStudiosRentals, postNewRental } from "./apiCalls";
import { generateRentedView } from "./generateRentedView";


export async function createFilmsGallery(allFilms:Film[]) {
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
      rentButton.onclick = async () => {
         const rentAction = await postNewRental(film.id);
         if (rentAction.status != 200) {
            alert(`${rentAction.data.message}`);
            return;
         }

         insertReturnButton(rentButton, copiesText, film);
         copiesText.innerText = `Antal tillgängliga exemplar: ${film.availableCopies - 1} st`;
      }

      if (studioRentals && rentedIds.includes(film.id)) {
         insertReturnButton(rentButton, copiesText, film);
      }
   });   
}


function insertReturnButton(rentButton:HTMLButtonElement, copiesText:HTMLTitleElement, film:Film) {
   rentButton.innerText = 'Lämna tillbaka';
   rentButton.onclick = () => {
      copiesText.innerText = `Antal tillgängliga exemplar: ${film.availableCopies - 1} st`;
   }
}
