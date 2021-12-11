import { FC } from "react";

const SearchChannels: FC = () => {
    return (
        <div>
            <input placeholder="Hledejte mezi kontakty"></input>
        </div>
    );
}

SearchChannels.displayName = "SearchChannelsComponent";
export default SearchChannels;