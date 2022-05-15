import React, {useEffect, useState} from 'react'
import Advertisement from './Advertisement';
import AdvertisementService from '../../Services/AdvertisementService'

export default function MyAdvertisements() {

    const [ads, setAds] = useState([]);

    const [message, setMessage] = useState("Loading...");

    useEffect(async () => {
        let ads = await AdvertisementService.getMyAdvertisments();

        if (ads.length > 0) {
            setAds(ads)
            return;
        }
        setMessage("You don't have any advertisements posted");
    }, [])
    
    return (
        <>
            {ads.length === 0 && (
                <h3>{message}</h3>
            )}
            <div className='d-flex justify-content-start flex-wrap'>
                {ads.map(ad => {
                    return <Advertisement ad={ad} isPersonal={false}/>
                })}
            </div>
        </>
    )
}
