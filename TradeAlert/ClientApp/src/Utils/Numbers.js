

export function Trunc2Decimal(num) {
    let numdecimals = Math.floor(num * 100) / 100
    return parseFloat(numdecimals.toFixed(2));
}