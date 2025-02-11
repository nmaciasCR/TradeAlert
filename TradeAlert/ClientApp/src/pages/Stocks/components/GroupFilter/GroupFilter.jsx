import React, { useEffect, useState, useCallback } from "react";
import Styles from "./GroupFilter.css";
import ListGroup from 'react-bootstrap/ListGroup';
import Form from 'react-bootstrap/Form';



const GroupFilter = ({ stockFiltersList, fnGroupFilters }) => {
    //Todos los grupos de la DDBB
    const [groupsList, setGroupsList] = useState([]);


    //Obtenemos todos los grupos de la DDBB
    const LoadGroupsList = useCallback(() => {
        fetch(`api/Groups/GetList`)
            .then(response => { return response.json() })
            .then(responseJson => {
                //Guardamos la LISTA DE GRUPOS
                setGroupsList(responseJson);
            })
            .catch(error => {
                console.log(error);
            });
    }, []);






    useEffect(() => {
        //Obtenemos el listado de grupos
        LoadGroupsList();
    }, [LoadGroupsList]);



    const CreateGroupLabel = (desc, qty) => {


        return `${desc} (${qty})`;
    }

    const CreateGroupListItem = (group) => {

        return (
            <ListGroup.Item key={group.id}>
                <div className="">
                    <Form.Check
                        type="checkbox"
                        id={group.id}
                        label={CreateGroupLabel(group.description, group.quoteQty)}
                        checked={stockFiltersList.groups.some(g => g === group.id)}
                        onChange={(event) => fnGroupFilters(event, group.id)}
                    />
                </div>
            </ListGroup.Item>

        )
    }


    return (
        <ListGroup className="group-filter-container">
            <ListGroup.Item className="title">Seleccione grupos para filtrar</ListGroup.Item>
            {
                groupsList.map(g => (CreateGroupListItem(g)))
            }
        </ListGroup>
    )
}


export default GroupFilter;