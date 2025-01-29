import React from "react";
import Form from 'react-bootstrap/Form';


export const TODO = 1;
export const REVISION = 2;


export const SelectFilterList = ({ valueSelected, onChangeEvent }) => {
    return (<div>
        <Form.Select value={valueSelected} onChange={onChangeEvent}>
            <option value={TODO} >Todo</option>
            <option value={REVISION}>Revision requerida</option>
        </Form.Select>
    </div>
    )


}