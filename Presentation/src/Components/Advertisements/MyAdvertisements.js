import React, {useEffect, useState} from 'react'
import { useParams } from "react-router-dom";
import Advertisement from './Advertisement';
import AdvertisementService from '../../Services/AdvertisementService'

export default function MyAdvertisements(props) {
    
    let { id } = useParams();
    
    const [ads, setAds] = useState({
        items:[],
        totalPages:0,
        currentPage:parseInt(id),
        pageSize:1,
        totalRecordsCount:0
    });

    const [message, setMessage] = useState("Loading...");

    useEffect(async () => {
        debugger;
        let pagedAds = await AdvertisementService.getMyAdvertisments(ads.currentPage, ads.pageSize);
        
        if (pagedAds.items.length > 0) {
            setAds(pagedAds)
            return;
        }
        setMessage("You don't have any advertisements posted");
    }, [])

    return (
        <>
            {ads.items.length === 0 && (
                <h3>{message}</h3>
            )}
            <div className='d-flex justify-content-start flex-wrap'>
                {ads.items.map(ad => {
                    return <Advertisement ad={ad} isPersonal={false}/>
                })}
            </div>

            <nav aria-label="Page navigation example">
                <ul className="pagination">
                    <li className="page-item"><a className="page-link" href="#">Previous</a></li>
                    {Array(ads.totalPages).fill(null).map((value, index) =>
                        <li className="page-item"><a className="page-link" href={'/my-advertisements/' + (index)}>{index + 1}</a></li>
                        )
                    }
                    <li className="page-item"><a className="page-link" href="#">Next</a></li>
                </ul>
            </nav>
        </>
    )
}
