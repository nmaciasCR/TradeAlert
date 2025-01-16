import arrowUp from "../images/quote_arrow_up.png";
import arrowDown from "../images/quote_arrow_down.png";
import arrowZero from "../images/quote_arrow_zero.png";




//Retorna la imagen de flecha en referencia el precio (positivo, negativo, netro)
export function GetArrowForPrice(price) {
    switch (Math.sign(price)) {
        case 1:
            return arrowUp;
        case -1:
            return arrowDown;
        default:
            return arrowZero;
    }
}