import React from 'react';



export function Trunc2Decimal(num) {
    let numdecimals = Math.floor(num * 100) / 100
    return numdecimals.toFixed(2);
}