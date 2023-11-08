import React from "react";
import Form from 'react-bootstrap/Form';
import InputGroup from 'react-bootstrap/InputGroup';
import searchIcon from "../../images/search_icon.png";


const SearchInput = ({ omChange }) => {



    return (
        <InputGroup>
            <InputGroup.Text id="basic-addon1">
                <img src={searchIcon} alt="" width="22" />
            </InputGroup.Text>
            <Form.Control
                placeholder="Simbolo o Nombre"
                aria-label="Username"
                aria-describedby="basic-addon1"
                onChange={omChange}
            />
        </InputGroup>
    )

}


export default SearchInput;