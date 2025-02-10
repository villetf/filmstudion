import { getAllFilms, getStudiosRentals } from "./apiCalls";
import { createFilmsGallery } from "./createFilmsGallery";
import { createNewElement } from "./createNewElement";
import { Film, Rental } from "./interfaces";
import { mainContent } from "./main";

export async function generateRentedView() {
   mainContent.innerHTML = '';
   createNewElement('h3', 'Dina hyrda filmer', null, 'text-2xl py-10', mainContent);
   const rentals:Rental[] = await getStudiosRentals();
   const rentedFilmsList:Film[] = []
   rentals.forEach(rental => {
      rentedFilmsList.push(rental.film);
   });

   if (rentedFilmsList.length > 0) {
      createFilmsGallery(rentedFilmsList);
   } else {
      createNewElement('h3', 'Du har inga hyrda filmer.', null, 'text-xl py-10', mainContent);
   }
}