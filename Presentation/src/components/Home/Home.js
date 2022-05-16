import React, {Component, useState, useEffect, useRef} from 'react'
import Advertisement from '../Advertisements/Advertisement';
import Filters from './Filters';
import Collapse from "react-bootstrap/Collapse";
import Button from 'react-bootstrap/Button'

export default function Home({ads, getAds}) {
    
    const [message, setMessage] = useState("Loading ...");
    const [isFiltersOpen, setIsFiltersOpen] = useState(false);
    
    useEffect(async () => {
        await getAds()
    },[]);
    
    useEffect(() => {
        if(ads.length > 0){
            setMessage("")
            return;
        }

        setMessage("There are no advertisements posted yet");
    }, [ads])
    
    function handleOpen() {
        setIsFiltersOpen(!isFiltersOpen);
    }

    return (
        <>
            <Button
                onClick={handleOpen}
                aria-controls="dropdown"
                aria-expanded={isFiltersOpen}>
                Open filters <i className={!isFiltersOpen ? "fa fa-angle-double-down" : "fa fa-angle-double-up"} style={{fontSize: '24px', margin: "3px 0 0 6px"}}></i>
            </Button>
            <Collapse in={isFiltersOpen}>
                <div className='mt-1' id="dropdown">
                    <Filters getAds={getAds}/>
                </div>
            </Collapse>
            
            {ads.length === 0 && (
                <h3>{message}</h3>
            )}
            {ads.length > 0 &&
                (
                    <div className='mt-4 d-flex justify-content-start flex-wrap'>
                        {ads.map(ad => {
                            return <Advertisement key={ad.id} ad={ad} isPersonal={true}/>
                        })}
                    </div>   
                )}
        </>
    )
}
