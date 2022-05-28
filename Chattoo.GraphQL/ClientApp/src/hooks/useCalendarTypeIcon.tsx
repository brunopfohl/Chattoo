import { Celebration, DirectionsRun, DirectionsWalk, QuestionMark, RecordVoiceOver, SportsBar, TheaterComedy, Theaters } from "@mui/icons-material";
import { CalendarEventTypeGraphType } from "graphql/graphql-types";

export const useCalendarEventTypeIconRenderer = () => {
    const renderIcon = (type: CalendarEventTypeGraphType | undefined | null) => {
        switch (type) {
            case CalendarEventTypeGraphType.Drink: {
                return <SportsBar />
            }
            case CalendarEventTypeGraphType.Celebration: {
                return <Celebration />
            }
            case CalendarEventTypeGraphType.Brainstorming: {
                return <RecordVoiceOver />
            }
            case CalendarEventTypeGraphType.Cinema: {
                return <Theaters />
            }
            case CalendarEventTypeGraphType.Sports: {
                return <DirectionsRun />
            }
            case CalendarEventTypeGraphType.Theatre: {
                return <TheaterComedy />
            }
            case CalendarEventTypeGraphType.Walk: {
                return <DirectionsWalk />
            }
            default: {
                <QuestionMark />
            }
        }
    };

    return renderIcon;
}