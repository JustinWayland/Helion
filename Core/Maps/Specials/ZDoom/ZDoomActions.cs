﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helion.Maps.Specials.ZDoom;

public static class ZDoomActions
{
    public static ZDoomLineSpecialType ParseZDoomAction(string action)
    {
        switch (action.ToLower())
        {
            case "polyobj_startline":
                return (ZDoomLineSpecialType)1;
            case "polyobj_rotateleft":
                return (ZDoomLineSpecialType)2;
            case "polyobj_rotateright":
                return (ZDoomLineSpecialType)3;
            case "polyobj_move":
                return (ZDoomLineSpecialType)4;
            case "polyobj_explicitline":
                return (ZDoomLineSpecialType)5;
            case "polyobj_movetimes8":
                return (ZDoomLineSpecialType)6;
            case "polyobj_doorswing":
                return (ZDoomLineSpecialType)7;
            case "polyobj_doorslide":
                return (ZDoomLineSpecialType)8;
            case "line_horizon":
                return (ZDoomLineSpecialType)9;
            case "door_close":
                return (ZDoomLineSpecialType)10;
            case "door_open":
                return (ZDoomLineSpecialType)11;
            case "door_raise":
                return (ZDoomLineSpecialType)12;
            case "door_lockedraise":
                return (ZDoomLineSpecialType)13;
            case "door_animated":
                return (ZDoomLineSpecialType)14;
            case "autosave":
                return (ZDoomLineSpecialType)15;
            case "transfer_walllight":
                return (ZDoomLineSpecialType)16;
            case "thing_raise":
                return (ZDoomLineSpecialType)17;
            case "startconversation":
                return (ZDoomLineSpecialType)18;
            case "thing_stop":
                return (ZDoomLineSpecialType)19;
            case "floor_lowerbyvalue":
                return (ZDoomLineSpecialType)20;
            case "floor_lowertolowest":
                return (ZDoomLineSpecialType)21;
            case "floor_lowertonearest":
                return (ZDoomLineSpecialType)22;
            case "floor_raisebyvalue":
                return (ZDoomLineSpecialType)23;
            case "floor_raisetohighest":
                return (ZDoomLineSpecialType)24;
            case "floor_raisetonearest":
                return (ZDoomLineSpecialType)25;
            case "stairs_builddown":
                return (ZDoomLineSpecialType)26;
            case "stairs_buildup":
                return (ZDoomLineSpecialType)27;
            case "floor_raiseandcrush":
                return (ZDoomLineSpecialType)28;
            case "pillar_build":
                return (ZDoomLineSpecialType)29;
            case "pillar_open":
                return (ZDoomLineSpecialType)30;
            case "stairs_builddownsync":
                return (ZDoomLineSpecialType)31;
            case "stairs_buildupsync":
                return (ZDoomLineSpecialType)32;
            case "forcefield":
                return (ZDoomLineSpecialType)33;
            case "clearforcefield":
                return (ZDoomLineSpecialType)34;
            case "floor_raisebyvaluetimes8":
                return (ZDoomLineSpecialType)35;
            case "floor_lowerbyvaluetimes8":
                return (ZDoomLineSpecialType)36;
            case "floor_movetovalue":
                return (ZDoomLineSpecialType)37;
            case "ceiling_waggle":
                return (ZDoomLineSpecialType)38;
            case "teleport_zombiechanger":
                return (ZDoomLineSpecialType)39;
            case "ceiling_lowerbyvalue":
                return (ZDoomLineSpecialType)40;
            case "ceiling_raisebyvalue":
                return (ZDoomLineSpecialType)41;
            case "ceiling_crushandraise":
                return (ZDoomLineSpecialType)42;
            case "ceiling_lowerandcrush":
                return (ZDoomLineSpecialType)43;
            case "ceiling_crushstop":
                return (ZDoomLineSpecialType)44;
            case "ceiling_crushraiseandstay":
                return (ZDoomLineSpecialType)45;
            case "floor_crushstop":
                return (ZDoomLineSpecialType)46;
            case "ceiling_movetovalue":
                return (ZDoomLineSpecialType)47;
            case "sector_attach3dmidtex":
                return (ZDoomLineSpecialType)48;
            case "glassbreak":
                return (ZDoomLineSpecialType)49;
            case "extrafloor_lightonly":
                return (ZDoomLineSpecialType)50;
            case "sector_setlink":
                return (ZDoomLineSpecialType)51;
            case "scroll_wall":
                return (ZDoomLineSpecialType)52;
            case "line_settextureoffset":
                return (ZDoomLineSpecialType)53;
            case "sector_changeflags":
                return (ZDoomLineSpecialType)54;
            case "line_setblocking":
                return (ZDoomLineSpecialType)55;
            case "line_settexturescale":
                return (ZDoomLineSpecialType)56;
            case "sector_setportal":
                return (ZDoomLineSpecialType)57;
            case "sector_copyscroller":
                return (ZDoomLineSpecialType)58;
            case "polyobj_or_movetospot":
                return (ZDoomLineSpecialType)59;
            case "plat_perpetualraise":
                return (ZDoomLineSpecialType)60;
            case "plat_stop":
                return (ZDoomLineSpecialType)61;
            case "plat_downwaitupstay":
                return (ZDoomLineSpecialType)62;
            case "plat_downbyvalue":
                return (ZDoomLineSpecialType)63;
            case "plat_upwaitdownstay":
                return (ZDoomLineSpecialType)64;
            case "plat_upbyvalue":
                return (ZDoomLineSpecialType)65;
            case "floor_lowerinstant":
                return (ZDoomLineSpecialType)66;
            case "floor_raiseinstant":
                return (ZDoomLineSpecialType)67;
            case "floor_movetovaluetimes8":
                return (ZDoomLineSpecialType)68;
            case "ceiling_movetovaluetimes8":
                return (ZDoomLineSpecialType)69;
            case "teleport":
                return (ZDoomLineSpecialType)70;
            case "teleport_nofog":
                return (ZDoomLineSpecialType)71;
            case "thrustthing":
                return (ZDoomLineSpecialType)72;
            case "damagething":
                return (ZDoomLineSpecialType)73;
            case "teleport_newmap":
                return (ZDoomLineSpecialType)74;
            case "teleport_endgame":
                return (ZDoomLineSpecialType)75;
            case "teleportother":
                return (ZDoomLineSpecialType)76;
            case "teleportgroup":
                return (ZDoomLineSpecialType)77;
            case "teleportinsector":
                return (ZDoomLineSpecialType)78;
            case "thing_setconversation":
                return (ZDoomLineSpecialType)79;
            case "acs_execute":
                return (ZDoomLineSpecialType)80;
            case "acs_suspend":
                return (ZDoomLineSpecialType)81;
            case "acs_terminate":
                return (ZDoomLineSpecialType)82;
            case "acs_lockedexecute":
                return (ZDoomLineSpecialType)83;
            case "acs_executewithresult":
                return (ZDoomLineSpecialType)84;
            case "acs_lockedexecutedoor":
                return (ZDoomLineSpecialType)85;
            case "polyobj_movetospot":
                return (ZDoomLineSpecialType)86;
            case "polyobj_stop":
                return (ZDoomLineSpecialType)87;
            case "polyobj_moveto":
                return (ZDoomLineSpecialType)88;
            case "polyobj_or_moveto":
                return (ZDoomLineSpecialType)89;
            case "polyobj_or_rotateleft":
                return (ZDoomLineSpecialType)90;
            case "polyobj_or_rotateright":
                return (ZDoomLineSpecialType)91;
            case "polyobj_or_move":
                return (ZDoomLineSpecialType)92;
            case "polyobj_or_movetimes8":
                return (ZDoomLineSpecialType)93;
            case "pillar_buildandcrush":
                return (ZDoomLineSpecialType)94;
            case "floorandceiling_lowerbyvalue":
                return (ZDoomLineSpecialType)95;
            case "floorandceiling_raisebyvalue":
                return (ZDoomLineSpecialType)96;
            case "ceiling_lowerandcrushdist":
                return (ZDoomLineSpecialType)97;
            case "sector_settranslucent":
                return (ZDoomLineSpecialType)98;
            case "floor_raiseandcrushdoom":
                return (ZDoomLineSpecialType)99;
            case "scroll_texture_left":
                return (ZDoomLineSpecialType)100;
            case "scroll_texture_right":
                return (ZDoomLineSpecialType)101;
            case "scroll_texture_up":
                return (ZDoomLineSpecialType)102;
            case "scroll_texture_down":
                return (ZDoomLineSpecialType)103;
            case "ceiling_crushandraisesilentdist":
                return (ZDoomLineSpecialType)104;
            case "door_waitraise":
                return (ZDoomLineSpecialType)105;
            case "door_waitclose":
                return (ZDoomLineSpecialType)106;
            case "line_setportaltarget":
                return (ZDoomLineSpecialType)107;
            case "light_forcelightning":
                return (ZDoomLineSpecialType)109;
            case "light_raisebyvalue":
                return (ZDoomLineSpecialType)110;
            case "light_lowerbyvalue":
                return (ZDoomLineSpecialType)111;
            case "light_changetovalue":
                return (ZDoomLineSpecialType)112;
            case "light_fade":
                return (ZDoomLineSpecialType)113;
            case "light_glow":
                return (ZDoomLineSpecialType)114;
            case "light_flicker":
                return (ZDoomLineSpecialType)115;
            case "light_strobe":
                return (ZDoomLineSpecialType)116;
            case "light_stop":
                return (ZDoomLineSpecialType)117;
            case "plane_copy":
                return (ZDoomLineSpecialType)118;
            case "thing_damage":
                return (ZDoomLineSpecialType)119;
            case "radius_quake":
                return (ZDoomLineSpecialType)120;
            case "line_setidentification":
                return (ZDoomLineSpecialType)121;
            case "thing_move":
                return (ZDoomLineSpecialType)125;
            case "thing_setspecial":
                return (ZDoomLineSpecialType)127;
            case "thrustthingz":
                return (ZDoomLineSpecialType)128;
            case "usepuzzleitem":
                return (ZDoomLineSpecialType)129;
            case "thing_activate":
                return (ZDoomLineSpecialType)130;
            case "thing_deactivate":
                return (ZDoomLineSpecialType)131;
            case "thing_remove":
                return (ZDoomLineSpecialType)132;
            case "thing_destroy":
                return (ZDoomLineSpecialType)133;
            case "thing_projectile":
                return (ZDoomLineSpecialType)134;
            case "thing_spawn":
                return (ZDoomLineSpecialType)135;
            case "thing_projectilegravity":
                return (ZDoomLineSpecialType)136;
            case "thing_spawnnofog":
                return (ZDoomLineSpecialType)137;
            case "floor_waggle":
                return (ZDoomLineSpecialType)138;
            case "thing_spawnfacing":
                return (ZDoomLineSpecialType)139;
            case "sector_changesound":
                return (ZDoomLineSpecialType)140;
            case "player_setteam":
                return (ZDoomLineSpecialType)145;
            case "team_score":
                return (ZDoomLineSpecialType)152;
            case "team_givepoints":
                return (ZDoomLineSpecialType)153;
            case "teleport_nostop":
                return (ZDoomLineSpecialType)154;
            case "line_setportal":
                return (ZDoomLineSpecialType)156;
            case "setglobalfogparameter":
                return (ZDoomLineSpecialType)157;
            case "fs_execute":
                return (ZDoomLineSpecialType)158;
            case "sector_setplanereflection":
                return (ZDoomLineSpecialType)159;
            case "sector_set3dfloor":
                return (ZDoomLineSpecialType)160;
            case "sector_setcontents":
                return (ZDoomLineSpecialType)161;
            case "ceiling_crushandraisedist":
                return (ZDoomLineSpecialType)168;
            case "generic_crusher2":
                return (ZDoomLineSpecialType)169;
            case "sector_setceilingscale2":
                return (ZDoomLineSpecialType)170;
            case "sector_setfloorscale2":
                return (ZDoomLineSpecialType)171;
            case "plat_upnearestwaitdownstay":
                return (ZDoomLineSpecialType)172;
            case "noisealert":
                return (ZDoomLineSpecialType)173;
            case "sendtocommunicator":
                return (ZDoomLineSpecialType)174;
            case "thing_projectileintercept":
                return (ZDoomLineSpecialType)175;
            case "thing_changetid":
                return (ZDoomLineSpecialType)176;
            case "thing_hate":
                return (ZDoomLineSpecialType)177;
            case "thing_projectileaimed":
                return (ZDoomLineSpecialType)178;
            case "changeskill":
                return (ZDoomLineSpecialType)179;
            case "thing_settranslation":
                return (ZDoomLineSpecialType)180;
            case "plane_align":
                return (ZDoomLineSpecialType)181;
            case "line_mirror":
                return (ZDoomLineSpecialType)182;
            case "line_alignceiling":
                return (ZDoomLineSpecialType)183;
            case "line_alignfloor":
                return (ZDoomLineSpecialType)184;
            case "sector_setrotation":
                return (ZDoomLineSpecialType)185;
            case "sector_setceilingpanning":
                return (ZDoomLineSpecialType)186;
            case "sector_setfloorpanning":
                return (ZDoomLineSpecialType)187;
            case "sector_setceilingscale":
                return (ZDoomLineSpecialType)188;
            case "sector_setfloorscale":
                return (ZDoomLineSpecialType)189;
            case "static_init":
                return (ZDoomLineSpecialType)190;
            case "setplayerproperty":
                return (ZDoomLineSpecialType)191;
            case "ceiling_lowertohighestfloor":
                return (ZDoomLineSpecialType)192;
            case "ceiling_lowerinstant":
                return (ZDoomLineSpecialType)193;
            case "ceiling_raiseinstant":
                return (ZDoomLineSpecialType)194;
            case "ceiling_crushraiseandstaya":
                return (ZDoomLineSpecialType)195;
            case "ceiling_crushandraisea":
                return (ZDoomLineSpecialType)196;
            case "ceiling_crushandraisesilenta":
                return (ZDoomLineSpecialType)197;
            case "ceiling_raisebyvaluetimes8":
                return (ZDoomLineSpecialType)198;
            case "ceiling_lowerbyvaluetimes8":
                return (ZDoomLineSpecialType)199;
            case "generic_floor":
                return (ZDoomLineSpecialType)200;
            case "generic_ceiling":
                return (ZDoomLineSpecialType)201;
            case "generic_door":
                return (ZDoomLineSpecialType)202;
            case "generic_lift":
                return (ZDoomLineSpecialType)203;
            case "generic_stairs":
                return (ZDoomLineSpecialType)204;
            case "generic_crusher":
                return (ZDoomLineSpecialType)205;
            case "plat_downwaitupstaylip":
                return (ZDoomLineSpecialType)206;
            case "plat_perpetualraiselip":
                return (ZDoomLineSpecialType)207;
            case "translucentline":
                return (ZDoomLineSpecialType)208;
            case "transfer_heights":
                return (ZDoomLineSpecialType)209;
            case "transfer_floorlight":
                return (ZDoomLineSpecialType)210;
            case "transfer_ceilinglight":
                return (ZDoomLineSpecialType)211;
            case "sector_setcolor":
                return (ZDoomLineSpecialType)212;
            case "sector_setfade":
                return (ZDoomLineSpecialType)213;
            case "sector_setdamage":
                return (ZDoomLineSpecialType)214;
            case "teleport_line":
                return (ZDoomLineSpecialType)215;
            case "sector_setgravity":
                return (ZDoomLineSpecialType)216;
            case "stairs_buildupdoom":
                return (ZDoomLineSpecialType)217;
            case "sector_setwind":
                return (ZDoomLineSpecialType)218;
            case "sector_setfriction":
                return (ZDoomLineSpecialType)219;
            case "sector_setcurrent":
                return (ZDoomLineSpecialType)220;
            case "scroll_texture_both":
                return (ZDoomLineSpecialType)221;
            case "scroll_texture_model":
                return (ZDoomLineSpecialType)222;
            case "scroll_floor":
                return (ZDoomLineSpecialType)223;
            case "scroll_ceiling":
                return (ZDoomLineSpecialType)224;
            case "scroll_texture_offsets":
                return (ZDoomLineSpecialType)225;
            case "acs_executealways":
                return (ZDoomLineSpecialType)226;
            case "pointpush_setforce":
                return (ZDoomLineSpecialType)227;
            case "plat_raiseandstaytx0":
                return (ZDoomLineSpecialType)228;
            case "thing_setgoal":
                return (ZDoomLineSpecialType)229;
            case "plat_upbyvaluestaytx":
                return (ZDoomLineSpecialType)230;
            case "plat_toggleceiling":
                return (ZDoomLineSpecialType)231;
            case "light_strobedoom":
                return (ZDoomLineSpecialType)232;
            case "light_minneighbor":
                return (ZDoomLineSpecialType)233;
            case "light_maxneighbor":
                return (ZDoomLineSpecialType)234;
            case "floor_transfertrigger":
                return (ZDoomLineSpecialType)235;
            case "floor_transfernumeric":
                return (ZDoomLineSpecialType)236;
            case "changecamera":
                return (ZDoomLineSpecialType)237;
            case "floor_raisetolowestceiling":
                return (ZDoomLineSpecialType)238;
            case "floor_raisebyvaluetxty":
                return (ZDoomLineSpecialType)239;
            case "floor_raisebytexture":
                return (ZDoomLineSpecialType)240;
            case "floor_lowertolowesttxty":
                return (ZDoomLineSpecialType)241;
            case "floor_lowertohighest":
                return (ZDoomLineSpecialType)242;
            case "exit_normal":
                return (ZDoomLineSpecialType)243;
            case "exit_secret":
                return (ZDoomLineSpecialType)244;
            case "elevator_raisetonearest":
                return (ZDoomLineSpecialType)245;
            case "elevator_movetofloor":
                return (ZDoomLineSpecialType)246;
            case "elevator_lowertonearest":
                return (ZDoomLineSpecialType)247;
            case "healthing":
                return (ZDoomLineSpecialType)248;
            case "door_closewaitopen":
                return (ZDoomLineSpecialType)249;
            case "floor_donut":
                return (ZDoomLineSpecialType)250;
            case "floorandceiling_lowerraise":
                return (ZDoomLineSpecialType)251;
            case "ceiling_raisetonearest":
                return (ZDoomLineSpecialType)252;
            case "ceiling_lowertolowest":
                return (ZDoomLineSpecialType)253;
            case "ceiling_lowertofloor":
                return (ZDoomLineSpecialType)254;
            case "ceiling_crushraiseandstaysila":
                return (ZDoomLineSpecialType)255;
            case "floor_lowertohighestee":
                return (ZDoomLineSpecialType)256;
            case "floor_raisetolowest":
                return (ZDoomLineSpecialType)257;
            case "floor_lowertolowestceiling":
                return (ZDoomLineSpecialType)258;
            case "floor_raisetoceiling":
                return (ZDoomLineSpecialType)259;
            case "floor_toceilinginstant":
                return (ZDoomLineSpecialType)260;
            case "floor_lowerbytexture":
                return (ZDoomLineSpecialType)261;
            case "ceiling_raisetohighest":
                return (ZDoomLineSpecialType)262;
            case "ceiling_tohighestinstant":
                return (ZDoomLineSpecialType)263;
            case "ceiling_lowertonearest":
                return (ZDoomLineSpecialType)264;
            case "ceiling_raisetolowest":
                return (ZDoomLineSpecialType)265;
            case "ceiling_raisetohighestfloor":
                return (ZDoomLineSpecialType)266;
            case "ceiling_tofloorinstant":
                return (ZDoomLineSpecialType)267;
            case "ceiling_raisebytexture":
                return (ZDoomLineSpecialType)268;
            case "ceiling_lowerbytexture":
                return (ZDoomLineSpecialType)269;
            case "stairs_builddowndoom":
                return (ZDoomLineSpecialType)270;
            case "stairs_buildupdoomsync":
                return (ZDoomLineSpecialType)271;
            case "stairs_builddowndoomsync":
                return (ZDoomLineSpecialType)272;
            case "stairs_buildupdoomcrush":
                return (ZDoomLineSpecialType)273;
            case "door_animatedclose":
                return (ZDoomLineSpecialType)274;
            case "floor_stop":
                return (ZDoomLineSpecialType)275;
            case "ceiling_stop":
                return (ZDoomLineSpecialType)276;
            case "sector_setfloorglow":
                return (ZDoomLineSpecialType)277;
            case "sector_setceilingglow":
                return (ZDoomLineSpecialType)278;
            case "floor_movetovalueandcrush":
                return (ZDoomLineSpecialType)279;
            case "ceiling_movetovalueandcrush":
                return (ZDoomLineSpecialType)280;
            case "line_setautomapflags":
                return (ZDoomLineSpecialType)281;
            case "line_setautomapstyle":
                return (ZDoomLineSpecialType)282;
            case "polyobj_stopsound":
                return (ZDoomLineSpecialType)283;
            case "generic_crusherdist":
                return (ZDoomLineSpecialType)284;

        }
        return ZDoomLineSpecialType.None;
    }
}