export interface Film {
   id: number,
   name: string,
   releaseYear: number,
   availableCopies: number
}

export interface Rental {
   rentalId: number,
   film: Film
}