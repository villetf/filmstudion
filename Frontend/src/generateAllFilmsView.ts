import { getAllFilms } from "./apiCalls";
import { createFilmsGallery } from "./createFilmsGallery";
import { createNewElement } from "./createNewElement";
import { Film } from "./interfaces";
import { mainContent } from "./main";

export async function generateAllFilmsView() {
   mainContent.innerHTML = '';
   createNewElement('h3', 'Alla filmer', null, 'text-2xl py-10', mainContent);
   const allFilms:Film[] = await getAllFilms();
   createFilmsGallery(allFilms);
}