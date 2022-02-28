import { Box, Button, Card, CardActionArea, CardActions, CardContent, CardMedia, Icon, LinearProgress, Stack, Typography } from "@mui/material";
import { FC, useCallback, useMemo } from "react";
import moment from "moment";
import { CalendarEvent } from "graphql/graphql-types";
import { useRouter } from "next/router";
import { Person } from "@mui/icons-material";
import { useCalendarEventTypeIconRenderer } from "@hooks/useCalendarTypeIcon";

export interface EventCardProps {
    calendarEvent: CalendarEvent;
}

const EventCard: FC<EventCardProps> = (props) => {
    const router = useRouter();
    const { calendarEvent } = props;

    const redirectToDetail = useCallback(() => {
        router.push({
            pathname: "/events/[id]",
            query: {
                id: calendarEvent.id
            }
        });
    }, [router]);

    const renderTypeIcon = useCalendarEventTypeIconRenderer();

    return (
        <Card sx={{ maxWidth: 345, m: 1 }}>
            <CardActionArea onClick={redirectToDetail}>
                <Box sx={{ position: "relative" }}>
                    <Box sx={{ bottom: "5px", left: "5px", borderRadius: "5px", p: 0.5, backgroundColor: "white", position: "absolute" }}>
                        {renderTypeIcon(calendarEvent.type)}
                    </Box>
                    <CardMedia
                        component="img"
                        alt="green iguana"
                        height="140"
                        image="/event.png"
                        sx={{ objectFit: "cover", borderRadius: "5px" }}
                    />
                </Box>
                <CardContent>
                    <Typography gutterBottom variant="caption" component="div">
                        {moment(calendarEvent.startsAt).format("d. MM. YYYY - hh:mm")}
                    </Typography>
                    <Typography gutterBottom variant="h5" component="div">
                        {calendarEvent.name}
                    </Typography>
                    <Stack direction="row" sx={{ alignItems: "center" }}>
                        <Icon sx={{ mr: 1 }}>
                            <Person />
                        </Icon>
                        {!!calendarEvent.maximalParticipantsCount
                            ? `${calendarEvent.participantsCount} / ${calendarEvent.maximalParticipantsCount}`
                            : `${calendarEvent.participantsCount}`
                        }
                    </Stack>
                </CardContent>
            </CardActionArea>
        </Card>
    );
};

EventCard.displayName = "EventsDashboardComponent";
export default EventCard;