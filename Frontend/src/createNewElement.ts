export function createNewElement(elementType:string, text:string|null, id:string|null, classes:string|null, parent:HTMLElement|null) {
   const element = document.createElement(elementType);

   if (text) {
      element.innerText = text;
   }

   if (id) {
      element.id = id;
   }

   if (classes) {
      element.classList.add(...classes.split(" "));
   }

   if (parent) {
      parent.appendChild(element);
   }
   return element;
}